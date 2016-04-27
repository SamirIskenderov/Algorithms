using Algorithms.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Library
{
    public class Generator
    {
        private readonly string n = Environment.NewLine;

        public IEnumerable<string> TestGenerate(Test test, bool mustFieldGenerate = true)
        {
            StringBuilder sb = new StringBuilder(128);

            if (mustFieldGenerate)
            {
                sb.Append(FieldGenerate(test));
            }

            foreach (var method in test.methodParamPair.Pairs)
            {
                sb.Append($"#region {method.Key.Split('.').Last().ToLower()}" + n + n);
                sb.Append("[TestMethod]" + n);
                sb.Append($"public void {method.Key.Split('.').Last().FirstLetterToUpper()}ingCorrect{test.TestEntity.FirstLetterToUpper()}MustNotThrowException()" + n);
                sb.Append("{" + n);
                sb.Append($"{method.Key}({test.HelperPath}.{test.TestEntity});" + n);
                sb.Append("}" + n + n);

                sb.Append("[TestMethod]" + n);
                sb.Append($"[ExpectedException(typeof(WriteCorrectExceptionHereManually))]" + n);
                sb.Append($"public void {method.Key.Split('.').Last().FirstLetterToUpper()}ingNull{test.TestEntity.FirstLetterToUpper()}MustThrowException()" + n);
                sb.Append("{" + n);
                sb.Append($"{method.Key}(null);" + n);
                sb.Append("}" + n + n);

                foreach (var methodTestCase in method.Value)
                {
                    foreach (var fieldTestCase in methodTestCase.FieldTestCases.Pairs)
                    {
                        foreach (var caseValueMatching in fieldTestCase.Value)
                        {
                            sb.Append("[TestMethod]" + n);

                            switch (methodTestCase.ExpectedResult)
                            {
                                case ExpectedResult.Exception:
                                    sb.Append($"[ExpectedException(typeof({methodTestCase.ExpectedResponse.FirstLetterToUpper()}))]" + n);
                                    break;

                                case ExpectedResult.Void:
                                case ExpectedResult.ReturnValue:
                                    break;

                                case ExpectedResult.None:
                                default:
                                    throw new ArgumentException("Incorrect expected result enum");
                            }

                            sb.Append($"public void {method.Key.Split('.').Last().FirstLetterToUpper()}ing");

                            if (methodTestCase.IsCorrectTest)
                            {
                                sb.Append("Correct");
                            }
                            else
                            {
                                sb.Append("Inorrect");
                            }

                            sb.Append(test.TestEntity.FirstLetterToUpper());

                            sb.Append("With");
                            sb.Append(fieldTestCase.Key.FirstLetterToUpper());

                            sb.Append(caseValueMatching.FirstLetterToUpper());

                            switch (methodTestCase.ExpectedResult)
                            {
                                case ExpectedResult.Void:
                                    sb.Append("MustNotThrowException");
                                    break;

                                case ExpectedResult.Exception:
                                    sb.Append($"MustThrow{methodTestCase.ExpectedResponse.FirstLetterToUpper()}");
                                    break;

                                case ExpectedResult.ReturnValue:

                                    sb.Append($"MustReturn{methodTestCase.ExpectedResponse.FirstLetterToUpper()}Value");
                                    break;

                                case ExpectedResult.None:
                                default:
                                    throw new ArgumentException("Incorrect expected result enum");
                            }

                            sb.Append("()" + n);

                            sb.Append("{" + n);

                            sb.Append($"var obj = {test.HelperPath}.{test.TestEntity}.Clone();" + n);

                            sb.Append($"obj.{fieldTestCase.Key} = {test.HelperPath}.{fieldTestCase.Key}_{caseValueMatching};" + n);

                            switch (methodTestCase.ExpectedResult)
                            {
                                case ExpectedResult.Void:
                                case ExpectedResult.Exception:
                                    sb.Append($"{method.Key}(obj);" + n);

                                    break;

                                case ExpectedResult.ReturnValue:
                                    sb.Append($"var result = {method.Key}(obj);" + n);
                                    sb.Append($"Assert.AreEqual({methodTestCase.ExpectedResponse}, result);" + n);
                                    break;

                                case ExpectedResult.None:
                                default:
                                    throw new ArgumentException("Incorrect expected result enum");
                            }

                            sb.Append("}" + n + n);
                        }
                    }
                }
                sb.Append($"#endregion {method.Key.Split('.').Last().ToLower()}" + n);
            }

            sb.Append(n);

            yield return sb.ToString();
        }

        private string FieldGenerate(Test test)
        {
            StringBuilder sb = new StringBuilder(128);

            sb.Append("#region configuration" + n);
            foreach (var method in test.methodParamPair.Pairs)
            {
                foreach (var methodTestCase in method.Value)
                {
                    foreach (var fieldTestCase in methodTestCase.FieldTestCases.Pairs)
                    {
                        sb.Append($"/// <summary>{n}/// ");

                        if (methodTestCase.IsCorrectTest)
                        {
                            sb.Append("Correct ");
                        }
                        else
                        {
                            sb.Append("Inorrect ");
                        }

                        sb.Append($"{fieldTestCase.Key} for {method.Key}(){n}");

                        switch (methodTestCase.ExpectedResult)
                        {
                            case ExpectedResult.Void:
                                sb.Append($"/// Nothing has to return{n}/// </summary>{n}");
                                break;

                            case ExpectedResult.Exception:
                                sb.Append($"/// Expecting {methodTestCase.ExpectedResponse.FirstLetterToUpper()}{n}/// </summary>{n}");
                                break;

                            case ExpectedResult.ReturnValue:
                                sb.Append($"/// Expect returning {methodTestCase.ExpectedResponse} value{n}/// </summary>{n}");
                                break;

                            case ExpectedResult.None:
                            default:
                                throw new ArgumentException("Incorrect expected result enum");
                        }

                        foreach (var caseValueMatching in fieldTestCase.Value)
                        {
                            sb.Append($"private static readonly string {fieldTestCase.Key}_{caseValueMatching} = \"\";" + n);
                        }

                        sb.Append(n);
                    }
                }
            }
            sb.Append("#endregion configuration" + n);

            return sb.ToString();
        }
    }
}