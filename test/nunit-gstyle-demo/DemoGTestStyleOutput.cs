using System;
using System.Threading;
using NUnit.Framework;

[assembly: GTestStyleConsoleOutputAttribute]
namespace nunit_gstyle_demo
{
  [TestFixture, GTestStyleConsoleOutputAttribute]
  public class SampleGTestStyleTest
  {
    [Test]
    public void SimpleTest()
    {
      Thread.Sleep(200);
      Assert.IsTrue(true);
    }

    [Test]
    public void SimpleFailedTest()
    {
      Thread.Sleep(500);
      Assert.IsTrue(false);
    }

    [Test]
    public void SimpleErrorTest()
    {
      Thread.Sleep(100);
      throw new Exception();
    }
  }

  [TestFixture, GTestStyleConsoleOutputAttribute]
  public class SampleGTestStyleTest2
  {
    [Test]
    public void SimpleTest()
    {
      Thread.Sleep(200);
      Assert.IsTrue(true);
    }

    [Test]
    public void SimpleFailedTest()
    {
      Thread.Sleep(500);
      Assert.IsTrue(false);
    }

    [Test]
    public void SimpleErrorTest()
    {
      Thread.Sleep(100);
      throw new Exception();
    }
  }
}
