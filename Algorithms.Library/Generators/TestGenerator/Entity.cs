using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Algorithms.Library
{
    public enum ExpectedResult
    {
        None = 0,
        Void = 1,
        Exception = 2,
        ReturnValue = 3,
    }

    /// <summary>
    /// Main entitiy to make conformity between testing method (key) and attributes of testing method (value).
    /// </summary>
    [Serializable]
    [JsonObjectAttribute]
    public class MethodParamPair
    {
        public Dictionary<string, List<MethodTestCases>> Pairs { get; set; }
    }

    /// <summary>
    /// Contains attribute for testing method.
    /// </summary>
    [Serializable]
    [JsonObjectAttribute]
    public class MethodTestCases
    {
        /// <summary>
        /// If this.ExpectedResult equals void, this field have to be empty.
        /// If this.ExpectedResult equals Exception, this field have to contains type name of expecting exception, e.g. "ArgumentException".
        /// If this.ExpectedResult equals ReturnValue, this field have to contains the value, e.g. "true".
        /// </summary>
        public string ExpectedResponse { get; set; }

        /// <summary>
        /// ExpectedResult.None will cause ArgumentException.
        /// </summary>
        public ExpectedResult ExpectedResult { get; set; }

        public FieldTestCases FieldTestCases { get; set; }

        /// <summary>
        /// Is test has to be positive or negative
        /// </summary>
        public bool IsCorrectTest { get; set; }
    }

    [Serializable]
    [JsonObjectAttribute]
    public class FieldTestCases : Dictionary<string, IEnumerable<string>>
    {
        public Dictionary<string, List<string>> Pairs { get; set; }
    }

    [Serializable]
    [JsonObjectAttribute]
    public class Test
    {
        /// <summary>
        /// HelperPath contains path to place, where you have testing values. Recomended to set it to your test class name.
        /// </summary>
        public string HelperPath { get; set; }

        /// <summary>
        /// Contains attribute for testing method.
        /// </summary>
        public MethodParamPair methodParamPair { get; set; }

        /// <summary>
        /// Entity name for testing.
        /// </summary>
        public string TestEntity { get; set; }
    }
}