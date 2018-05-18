﻿// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="TextRenderer.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.NetStandard.Execution.Rendering
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using StoryQ.NetStandard.Infrastructure;

    /// <summary>
    /// Class TextRenderer.
    /// </summary>
    /// <seealso cref="IRenderer" />
    internal class TextRenderer : IRenderer
    {
        /// <summary>
        /// The output
        /// </summary>
        private readonly TextWriter output;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRenderer"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public TextRenderer(TextWriter output)
        {
            this.output = output;

        }

        /// <summary>
        /// Renders the results.
        /// </summary>
        /// <param name="results">The results.</param>
        public void Render(IEnumerable<Result> results)
        {
            using (var buffer = new StringWriter())
            {
                var exceptionTable = results.Where(x => x.Type == ResultType.Failed).Select(x => x.Exception).ToList();
                var messages = results.Select(r => new
                {
                    Result = r,
                    Description = new string(' ', 2 * r.IndentLevel) + r.Prefix + " " + r.Text
                }).ToList();

                var messageLength = messages.Max(x => x.Description.Length);

                foreach (var m in messages)
                {
                    var r = m.Result;

                    if (this.ShouldPutNewlineBefore(r))
                    {
                        buffer.WriteLine();
                    }

                    buffer.Write(m.Description);

                    if (r.Type != ResultType.NotExecutable)
                    {
                        // padding
                        buffer.Write(new string(' ', messageLength - m.Description.Length));
                        buffer.Write(" => ");
                        buffer.Write(r.Type);
                        if (r.Type == ResultType.Pending)
                        {
                            buffer.Write(" !!");
                        }
                        else if (r.Type == ResultType.Failed)
                        {
                            buffer.Write(": \"");
                            buffer.Write(r.Exception.Message);
                            buffer.Write(" [");
                            buffer.Write(exceptionTable.IndexOf(r.Exception) + 1);
                            buffer.Write("]\"");
                        }
                    }

                    var tags = r.Tags.Select(x => "#" + x).Join(", ");
                    if (!string.IsNullOrEmpty(tags))
                    {
                        buffer.Write(" => (");
                        buffer.Write(tags);
                        buffer.Write(")");
                    }


                    buffer.WriteLine();
                }

                if (exceptionTable.Count > 0)
                {
                    buffer.WriteLine();
                    buffer.WriteLine("_______________________");
                    buffer.WriteLine("Full exception details:");
                    buffer.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");

                    for (int i = 0; i < exceptionTable.Count; i++)
                    {
                        buffer.WriteLine("[{0}]: {1}", (i + 1), exceptionTable[i]);
                        buffer.WriteLine();
                    }
                }

                this.output.Write(buffer.ToString());
            }
        }

        /// <summary>
        /// Shoulds the put newline before.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected virtual bool ShouldPutNewlineBefore(Result r) => r.IndentLevel == 3;
    }
}
