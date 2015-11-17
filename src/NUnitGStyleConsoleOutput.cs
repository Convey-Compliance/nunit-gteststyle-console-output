using System;

namespace NUnit.Framework
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class |
                  AttributeTargets.Interface | AttributeTargets.Assembly,
    AllowMultiple = true)]
  public class GTestStyleConsoleOutputAttribute : Attribute, ITestAction
  {
    private int _startTickCount;

    public void BeforeTest(TestDetails details)
    {
      _startTickCount = Environment.TickCount;
      if (details.IsSuite)
      {
        Console.WriteLine("[----------] running tests from {0}", details.Fixture != null ? details.Fixture.GetType().Name : "{no fixture}");
      }
      else
      {
        Console.WriteLine("[ RUN      ] {0}", details.Method != null ? details.Method.Name : "{no method}");
      }

    }

    public void AfterTest(TestDetails details)
    {
      if (details.IsSuite)
      {
        Console.WriteLine("[==========] finished tests from {0} ({1} ms total)", details.Fixture != null ? details.Fixture.GetType().Name : "{no fixture}", Environment.TickCount - _startTickCount);
      }
      else
      {
        string stateStr;
        switch (TestContext.CurrentContext.Result.State)
        {
          case TestState.Inconclusive:
            stateStr = "  INCONCL ";
            break;
          case TestState.NotRunnable:
            stateStr = "  CANTRUN ";
            break;
          case TestState.Skipped:
            stateStr = "  SKIPPED ";
            break;
          case TestState.Ignored:
            stateStr = "  IGNORED ";
            break;
          case TestState.Success:
            stateStr = "       OK ";
            break;
          case TestState.Failure:
            stateStr = "     FAIL ";
            break;
          case TestState.Error:
            stateStr = "    ERROR ";
            break;
          case TestState.Cancelled:
            stateStr = "   CANCEL ";
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        {
          Console.WriteLine("[{0}] {1} ({2} ms total)", stateStr, details.Method != null ? details.Method.Name : "{no method}", Environment.TickCount - _startTickCount);
        }
      }
    }

    public ActionTargets Targets
    {
      get { return ActionTargets.Test | ActionTargets.Suite; }
    }
  }
}
