#region License Statement
// Copyright (c) L.A.B.Soft.  All rights reserved.
//
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (https://opensource.org/licenses/cpl1.0.php)
// which can be found in the file cpl1.0.txt at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
#endregion


namespace Textile.States
{
    /// <summary>
    /// Formatting state for headers and titles.
    /// </summary>
    [FormatterState(SimpleBlockFormatterState.TextilePatternBegin + @"h[0-9]+" + SimpleBlockFormatterState.TextilePatternEnd)]
    public class HeaderFormatterState : SimpleBlockFormatterState
    {
        private static readonly Regex HeaderRegex = new Regex(@"^h(?<lvl>[0-9]+)");

        private int m_headerLevel = 0;
        public int HeaderLevel
        {
            get { return m_headerLevel; }
        }

		public HeaderFormatterState()
        {
        }

        public override void Enter()
        {
            Formatter.Output.Write(string.Format("<h{0}{1}>", HeaderLevel, FormattedStylesAndAlignment()));
        }

        public override void Exit()
        {
            Formatter.Output.WriteLine(string.Format("</h{0}>", HeaderLevel.ToString()));
        }

        protected override void OnContextAcquired()
        {
            Match m = HeaderRegex.Match(Tag);
            m_headerLevel = Int32.Parse(m.Groups["lvl"].Value);
        }

        public override void FormatLine(string input)
        {
            Formatter.Output.Write(input);
        }

		public override bool ShouldExit(string intput, string inputLookAhead)
        {
            return true;
        }

        public override bool ShouldNestState(FormatterState other)
        {
            return false;
        }
    }
}
