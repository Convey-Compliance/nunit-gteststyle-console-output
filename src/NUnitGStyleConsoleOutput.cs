using System;
using System.Collections.Generic;

namespace NUnit.Framework
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
  public class GTestStyleConsoleOutputAttribute : Attribute, ITestAction
  {
    private static int _totalTestCount;
    private static string _lastBeforeTestFullName;
    private static string _lastAfterTestFullName;
    private static readonly Dictionary<string, int> _testsProcessed = new Dictionary<string, int>();
    private static readonly Dictionary<string, int> _testsCount = new Dictionary<string, int>();

    public void BeforeTest(TestDetails details)
    {
      if (_lastBeforeTestFullName == details.FullName) return;
      _lastBeforeTestFullName = details.FullName;
      if (details.Fixture == null)
      {
        _totalTestCount = 0;
        _testsProcessed.Clear();
        _testsCount.Clear();
      }
      var startTickCount = Environment.TickCount;
      _testsProcessed.Add(details.FullName, startTickCount);
      if (details.IsSuite)
      {
        if (details.Fixture != null)
          _testsCount.Add(details.Fixture.GetType().Name, 0);
        Console.WriteLine("[----------] running tests from {0}", details.Fixture != null ? details.Fixture.GetType().Name : details.FullName);
      }
      else
      {
        if (details.Fixture != null)
        {
          int count;
          if (_testsCount.TryGetValue(details.Fixture.GetType().Name, out count))
          {
            _testsCount.Remove(details.Fixture.GetType().Name);
            _testsCount.Add(details.Fixture.GetType().Name, count + 1);
          }
        }
        _totalTestCount++;
        Console.WriteLine("[ RUN      ] {0}.{1}", 
                          details.Fixture != null ? details.Fixture.GetType().Name : "<no class>", 
                          details.Method != null ? details.Method.Name : "{no method}");
      }
    }

    public void AfterTest(TestDetails details)
    {
      if (_lastAfterTestFullName == details.FullName) return;
      _lastAfterTestFullName = details.FullName;
      int startTickCount;
      if (!_testsProcessed.TryGetValue(details.FullName, out startTickCount))
        return;
      if (details.IsSuite)
      {
        int count;
        if (details.Fixture != null)
          _testsCount.TryGetValue(details.Fixture.GetType().Name, out count);
        else count = _totalTestCount;
        Console.WriteLine("[==========] {0} test from {1} ({2} ms total)", 
                          count,
                          details.Fixture != null ? details.Fixture.GetType().Name : details.FullName, 
                          Environment.TickCount - startTickCount);
        Console.WriteLine("");
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
          Console.WriteLine("[{0}] {1}.{2} ({3} ms total)", 
                            stateStr, 
                            details.Fixture != null ? details.Fixture.GetType().Name : "<no class>", 
                            details.Method != null ? details.Method.Name : "{no method}", 
                            Environment.TickCount - startTickCount);
        }
      }
    }

    public ActionTargets Targets
    {
      get { return ActionTargets.Test | ActionTargets.Suite; }
    }
  }
}
