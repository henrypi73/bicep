// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bicep.Core.Parser;
using Bicep.Core.Diagnostics;

namespace Bicep.Core.Syntax
{
    public class ProgramSyntax : SyntaxBase
    {
        public ProgramSyntax(IEnumerable<SyntaxBase> statements, Token endOfFile, IEnumerable<Diagnostic> lexerDiagnostics)
        {
            this.Statements = statements.ToList().AsReadOnly();
            this.EndOfFile = endOfFile;
            this.LexerDiagnostics = lexerDiagnostics.ToImmutableArray();
        }

        public IReadOnlyList<SyntaxBase> Statements { get; }

        public Token EndOfFile { get; }

        public ImmutableArray<Diagnostic> LexerDiagnostics { get; }

        public override void Accept(SyntaxVisitor visitor)
            => visitor.VisitProgramSyntax(this);

        public override TextSpan Span
            => Statements.Any() ? 
                TextSpan.Between(Statements.First(), EndOfFile) :
                TextSpan.Between(EndOfFile, EndOfFile);
    }
}
