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


namespace Textile
{
    /// <summary>
    /// Base class for formatter states.
    /// </summary>
    /// A formatter state describes the current situation
    /// of the text being currently processed. A state can
    /// write HTML code when entered, exited, and can modify
    /// each line of text it receives.
    public abstract class FormatterState
    {
        /// <summary>
        /// A convenience property to get whether the owner <see cref="GenericFormatter"/>
        /// has the 'restrcted' mode turned on. If there is no owning formatter, 
        /// return false.
        /// </summary>
        protected bool UseRestrictedMode
        {
            get
            {
                if (m_formatter != null)
                    return m_formatter.UseRestrictedMode;
                return false;
            }
        }

        private GenericFormatter m_formatter;
        /// <summary>
        /// Gets the formatter this state belongs to.
        /// </summary>
        public GenericFormatter Formatter
        {
            get { return m_formatter; }
			internal set { m_formatter = value; }
        }

        /// <summary>
        /// Gets or sets whether the formatter is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Constructs a new instance of <see cref="FormatterState"/>.
        /// </summary>
        protected FormatterState()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract string Consume(FormatterStateConsumeContext context);

        /// <summary>
        /// Method called when the state is entered.
        /// </summary>
        public abstract void Enter();
        /// <summary>
        /// Method called when the state is exited.
        /// </summary>
        public abstract void Exit();
        /// <summary>
        /// Method called when a line of text should be written
        /// to the web form.
        /// </summary>
        /// <param name="input">The line of text.</param>
        public abstract void FormatLine(string input);

        /// <summary>
        /// Returns whether this state can last for more than one line.
        /// </summary>
        /// <returns>A boolean value stating whether this state is only for one line.</returns>
        /// This method should return true only if this state is genuinely
        /// multi-line. For example, a header text is only one line long. You can
        /// have several consecutive lines of header texts, but they are not the same
        /// header - just several headers one after the other.
        /// Bulleted and numbered lists are good examples of multi-line states.
        //abstract public bool IsOneLineOnly();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract bool ShouldExit(string input, string inputLookAhead);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualTag"></param>
        /// <param name="alignNfo"></param>
        /// <param name="attNfo"></param>
        /// <returns></returns>
        public virtual bool ShouldNestState(FormatterState other)
        {
            return false;
        }

        /// <summary>
        /// Returns whether block formatting (quick phrase modifiers, etc.) should be
        /// applied to this line.
        /// </summary>
        /// <param name="input">The line of text</param>
        /// <returns>Whether the line should be formatted for blocks</returns>
        public virtual bool ShouldFormatBlocks(string input)
        {
            return true;
        }

        /// <summary>
        /// Returns whether the current state accepts being superceded by another one
        /// we would possibly find by parsing the input line of text.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual bool ShouldParseForNewFormatterState(string input)
        {
            return true;
        }

        /// <summary>
        /// Returns whether post-processors should be applied to this line.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual bool ShouldPostProcess(string input)
        {
            return true;
        }

        /// <summary>
        /// Gets the formatting state we should fallback to if we don't find anything
        /// relevant in a line of text.
        /// </summary>
        public virtual Type FallbackFormattingState
        {
            get
            {
                return typeof(States.ParagraphFormatterState);
            }
        }

        protected FormatterState CurrentFormatterState
        {
            get { return this.Formatter.CurrentState; }
        }

        protected void ChangeFormatterState(FormatterState formatterState)
        {
            this.Formatter.ChangeState(formatterState);
        }
    }
}
