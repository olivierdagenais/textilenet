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
	/// Formatting state for a numbered list.
	/// </summary>
    [FormatterState(ListFormatterState.PatternBegin + @"#+" + ListFormatterState.PatternEnd)]
	public class OrderedListFormatterState : ListFormatterState
	{
		public OrderedListFormatterState()
		{
		}

		protected override void WriteIndent()
		{
            Formatter.Output.WriteLine("<ol" + FormattedStylesAndAlignment() + ">");
		}

		protected override void WriteOutdent()
		{
			Formatter.Output.WriteLine("</ol>");
		}

        protected override bool IsMatchForMe(string input, int minNestingDepth, int maxNestingDepth)
        {
            return Regex.IsMatch(input, @"^\s*([\*#]{" + (minNestingDepth - 1) + @"," + (maxNestingDepth - 1) + @"})#" + TextileGlobals.BlockModifiersPattern + @"\s");
        }

        protected override bool IsMatchForOthers(string input, int minNestingDepth, int maxNestingDepth)
        {
            return Regex.IsMatch(input, @"^\s*([\*#]{" + (minNestingDepth - 1) + @"," + (maxNestingDepth - 1) + @"})\*" + TextileGlobals.BlockModifiersPattern + @"\s");
        }
	}
}
