using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeApp.Tests
{
    [TestFixture]
    public class BarcodeScannerTests
    {
        [Test]
        public void TestScanBarcode()
        {
            // Arrange
            string expectedBarcode = "1234567890";

            // Act
            string actualBarcode = MainActivity.ScanBarcode();

            // Assert
            Assert.AreEqual(expectedBarcode, actualBarcode);
        }

        [Test]
        public void TestSendToApi()
        {
            // Arrange
            string barcode = "1234567890";
            string expectedResponse = "Success";

            // Act
            string actualResponse = MainActivity.SendToApi(barcode);

            // Assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}
