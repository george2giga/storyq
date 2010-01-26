﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using StoryQ.Formatting.Methods;
using StoryQ.Formatting.Parameters;

namespace StoryQ.Formatting
{
    /// <summary>
    /// A StoryQ infrastructure class that can format a given StoryQ Test method into a human-friendly (even if the human
    /// in question isn't a developer!) string
    /// </summary>
    public static class Formatter
    {
        private static readonly ParametersInlineMethodFormatAttribute underscores = new ParametersInlineMethodFormatAttribute();
        private static readonly ParameterSuffixedMethodFormatAttribute noUnderscores = new ParameterSuffixedMethodFormatAttribute();

        /// <summary>
        /// Formats a method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>a string representing the method's description</returns>
        public static string FormatMethod(Delegate method, params object[] arguments)
        {
            ParameterInfo[] parameterInfos = method.Method.GetParameters();
            Debug.Assert(parameterInfos.Length == arguments.Length);
            var argsAsStrings = arguments.Select((x, i) => FormatParameter(parameterInfos[i], x));

            MethodFormatAttribute formatter = GetFormatter(method);
            return formatter.Format(method.Method, argsAsStrings);
        }

        private static MethodFormatAttribute GetFormatter(Delegate method)
        {
            var formatter = method.Method.GetCustomAttributes(true)
                .OfType<MethodFormatAttribute>()
                .FirstOrDefault();

            if (formatter != null)
            {
                return formatter;
            }

            bool anyUnderscores = method.Method.Name.Contains("_");
            return anyUnderscores ? (MethodFormatAttribute) underscores : noUnderscores;
        }

        private static string FormatParameter(ParameterInfo info, object value)
        {
            var a = info.GetCustomAttributes(true)
                        .OfType<ParameterFormatAttribute>()
                        .FirstOrDefault();

            return a == null ? "" + value : a.Format(value);
        }
    }
}
