using NUnit.Framework;

/*[assembly: GTestStyleConsoleOutputAttribute]*/
namespace nunit_gstyle_demo
{
  [TestFixture, GTestStyleConsoleOutputAttribute]
  class SampleGTestStyleTest
  {
    [Test]
    public void SimpleTest()
    {
      Assert.IsTrue(true);
    }

    [Test]
    public void SimpleFailedTest()
    {
      Assert.IsTrue(false);
    }
  }
}
