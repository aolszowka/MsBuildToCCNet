// <copyright file="Project.cs" company="Christian Rodemeyer">
// Copyright © 2006 by Christian Rodemeyer (mailto:christian@atombrenner.de)
// Additions © 2017 by Ace Olszowka (GitHub @aolszowka)
// </copyright>

namespace Rodemeyer.MsBuildToCCNet
{
    using System.Collections.Generic;
    using Microsoft.Build.Framework;

    internal class Project
    {
        internal Project(string file)
        {
            this.File = file;

            this.errors = new List<Error>();
            this.warnings = new List<Warning>();
            this.messages = new List<Message>();
        }

        public readonly string File;

        public IEnumerable<Error> Errors
        {
            get { return this.errors; }
        }

        private List<Error> errors;

        public IEnumerable<Warning> Warnings
        {
            get { return this.warnings; }
        }

        private List<Warning> warnings;

        public IEnumerable<Message> Messages
        {
            get { return this.messages; }
        }

        private List<Message> messages;

        public int ErrorCount
        {
            get { return this.errors.Count; }
        }

        public int WarningCount
        {
            get { return this.warnings.Count; }
        }

        public int MessageCount
        {
            get { return this.messages.Count; }
        }

        public void Add(Error e)
        {
            this.errors.Add(e);
        }

        public void Add(Warning w)
        {
            this.warnings.Add(w);
        }

        public void Add(Message m)
        {
            this.messages.Add(m);
        }
    }

    internal class ErrorOrWarningBase
    {
        protected ErrorOrWarningBase(string code, string text, string file, int line, int column)
        {
            this.Code = code;
            this.Text = text;
            this.File = file == string.Empty ? null : file;
            this.Line = line;
            this.Column = column;
        }

        public readonly string Code;
        public readonly string Text;
        public readonly string File;
        public readonly int Line;
        public readonly int Column;
    }

    internal class Warning : ErrorOrWarningBase
    {
        public Warning(BuildWarningEventArgs e)
            : base(e.Code, e.Message, e.File, e.LineNumber, e.ColumnNumber)
        {
        }
    }

    internal class Error : ErrorOrWarningBase
    {
        public Error(BuildErrorEventArgs e)
            : base(e.Code, e.Message, e.File, e.LineNumber, e.ColumnNumber)
        {
        }
    }

    internal class Message
    {
        public Message(BuildMessageEventArgs e)
        {
            this.Importance = e.Importance;
            this.Text = e.Message;
        }

        public readonly string Text;
        public readonly MessageImportance Importance;
    }
}
