using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities; 

namespace WebApplication1
{
    public class ReceiptPrinterService
    {
        public void PrintReceipt(OrderDto order)
        {
            // 1. Initialize the printer connection (Network printer via IP)
            // Note: For USB or Serial, use SerialPrinter or UsbPrinter classes instead
            // Change "COM5" to whatever port your printer is assigned to
            using var printer = new SerialPrinter(portName: "COM5", baudRate: 115200);
            // 2. Initialize the emitter (EPSON is standard, but works with most thermal printers)
            var e = new EPSON();

            // 3. Build the receipt byte array using the fluent API
            byte[] receipt = ByteSplicer.Combine(
                e.CenterAlign(),
                e.PrintLine("MY AWESOME STORE"),
                e.PrintLine("123 Main Street"),
                e.PrintLine("--------------------------------"),
                e.LeftAlign(),
                e.PrintLine($"Order ID: {order.Id}"),
                e.PrintLine("--------------------------------"),
                // Add items...
                e.PrintLine("Coffee         $3.50"),
                e.PrintLine("Donut          $1.50"),
                e.PrintLine("--------------------------------"),
                e.RightAlign(),
                e.PrintLine("TOTAL: $5.00"),
                e.PrintLine(""),
                e.CenterAlign(),
                e.PrintLine("Thank you for your business!"),
                e.FeedLines(3),
                e.FullCut()
            );

            // 4. Send to the printer
            printer.Write(receipt);
        }
    }
}