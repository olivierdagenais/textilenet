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
    /// Formatting state for a standard text (i.e. just paragraphs).
    /// </summary>
    [FormatterState(SimpleBlockFormatterState.TextilePatternBegin + @"p" + SimpleBlockFormatterState.TextilePatternEnd)]
    public class ParagraphFormatterState : SimpleBlockFormatterState
    {
        public ParagraphFormatterState()
        {
        }

        public override void Enter()
        {
            Formatter.Output.Write("<p" + FormattedStylesAndAlignment() + ">");
        }

        public override void Exit()
        {
            Formatter.Output.WriteLine("</p>");
        }

        public override void FormatLine(string input)
        {
            Formatter.Output.Write(input);
        }

		public override bool ShouldExit(string input, string inputLookAhead)
        {
            if (Regex.IsMatch(input, @"^\s*$"))
                return true;
            Formatter.Output.WriteLine("<br />");
            return false;
        }

        public override bool ShouldNestState(FormatterState other)
        {
            return false;
        }
    }
}
