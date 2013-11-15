using System;
using System.Linq;

namespace ReusableToolkits.Implementations
{
  public class GuardUtility
  {
    private readonly string _className;

    /// <summary>
    /// Constructor, accept the class name of the class to guard.
    /// </summary>
    /// <param name="className"></param>
    public GuardUtility(string className)
    {
      string methodName = "GuardUtility.GuardUtility(string className)";
      if (string.IsNullOrEmpty(className))
      {
        throw new ArgumentNullException(className, methodName);
      }
      if (className.Trim().Length <= 0)
      {
        throw new ArgumentNullException(className, methodName);
      }
      _className = className;
    }

    /// <summary>
    /// Get the full method name in [Class].[MethodName()] format.
    /// </summary>
    /// <param name="shortMethodName">The method name</param>
    /// <returns>Returns the full method name.</returns>
    public string GetFullMethodName(string shortMethodName)
    {
      return string.Format("{0}.{1}", _className, shortMethodName);
    }

    /// <summary>
    /// Make sure the param value is contains in the specified set.
    /// This method will throw an ArgumentException if the parameter value is not in the specified set.
    /// </summary>
    /// <param name="param">The parameter value.</param>
    /// <param name="varName">The parameter name.</param>
    /// <param name="set">The set containing valid values.</param>
    public void GuardParamInSet(object param, string varName, object[] set)
    {
      if (!set.Contains(param))
      {
        string allValues = string.Empty;
        foreach (object o in set)
        {
          if (allValues.Length > 0)
          {
            allValues += ",";
          }
          allValues += o.ToString();
        }
        throw new ArgumentException(string.Format("Parameter value must be one of these values: {0}", allValues),
                                    varName);
      }
      ;
    }

    /// <summary>
    /// Make sure the parameter value is not null.
    /// This method will throw an ArgumentNullException if the parameter value is null.
    /// </summary>
    /// <param name="param">The parameter value.</param>
    /// <param name="varName">The parameter name.</param>
    /// <param name="methodName">The short method name.</param>
    public void GuardParamNotNull(object param, string varName, string methodName)
    {
      if (param == null)
      {
        throw new ArgumentNullException(varName, GetFullMethodName(methodName));
      }
    }

    public void GuardReturnValueMaxLength(string retVal, int maxLength, string varName, string methodName)
    {
      if (!string.IsNullOrEmpty(retVal))
      {
        if (retVal.Length > maxLength)
        {
          throw new ApplicationException(string.Format("{2}, {0}.Length must not exceed: {1}", varName, maxLength,
                                                       GetFullMethodName(methodName)));
        }
      }
    }

    public void GuardReturnValueNotNull(object retVal, string varName, string methodName)
    {
      if (retVal == null)
      {
        string errorMessage = string.Format("Return value {0} must not null. MethodName: {1}", varName,
                                            GetFullMethodName(methodName));
        throw new InvalidOperationException(errorMessage);
      }
    }

    public void GuardReturnValueNotEmpty(object retVal, string varName, string methodName)
    {
      GuardReturnValueNotNull(retVal, varName, methodName);
      if (retVal.ToString().Trim().Length <= 0)
      {
        string errorMessage = string.Format("Return value {0} must not empty. MethodName: {1}", varName,
                                            GetFullMethodName(methodName));
        throw new InvalidOperationException(errorMessage);
      }
    }

    public void GuardParamStringNotEmpty(string param, string varName, string methodName)
    {
      GuardParamNotNull(param, varName, methodName);
      if (param.Trim().Length <= 0)
      {
        throw new ArgumentException(
          "Must not empty or only containing white space(s). " + GetFullMethodName(methodName), varName);
      }
    }

    public void GuardParamDateTimeNotEmpty(DateTime param, string varName, string methodName)
    {
      if (param == DateTimeValues.EmptyValue)
      {
        throw new ArgumentException(
          "Must not empty or only containing white space(s). " + GetFullMethodName(methodName), varName);
      }
    }

    public T GuardMustNotReachHere<T>(string methodName)
    {
      string fullMethodName = GetFullMethodName(methodName);
      throw new InvalidOperationException("Must not reach here (" + fullMethodName + ")");
    }

    public void GuardParamLengtMustBeGTThan( int p, object[] args, string p_3, string methodName )
    {
      if (args.Length <= p)
      {
        throw new InvalidOperationException(p_3 + " length must be greater than " + p.ToString() + "." + GetFullMethodName(methodName));
      }
    }
  }
}