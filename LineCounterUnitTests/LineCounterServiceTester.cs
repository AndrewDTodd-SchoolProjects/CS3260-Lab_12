using DiskFileLineCounterMAUI.Services;

namespace LineCounterUnitTests
{
    [TestClass]
    public class LineCounterServiceTester
    {
        [TestMethod]
        public void CountLinesAsyncTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await LineCounterService.CountLinesAsync(null));
            Assert.ThrowsExceptionAsync<FileNotFoundException>(async () => await LineCounterService.CountLinesAsync("BadFileName"));
            Assert.ThrowsExceptionAsync<DirectoryNotFoundException>(async () => await LineCounterService.CountLinesAsync("Z:\\BadDirectory"));

            int lineCount = LineCounterService.CountLinesAsync("../../../../TestData.txt").Result;


            Assert.AreEqual(35, lineCount);
        }
    }
}