using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace Analyzer1
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer1Analyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "Analyzer1";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = "Remove comment";
        private static readonly LocalizableString MessageFormat = "Comment can be removed";
        private static readonly LocalizableString Description = "There should be no comments";

        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat,
            Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(Rule); }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxTreeAction(AnalyzeComment);
        }

        private void AnalyzeComment(SyntaxTreeAnalysisContext context)
        {
            SyntaxNode root = context.Tree.GetCompilationUnitRoot(context.CancellationToken);
            var commentNodes = from node in root.DescendantTrivia()
                where node.IsKind(SyntaxKind.MultiLineCommentTrivia) || node.IsKind(SyntaxKind.SingleLineCommentTrivia)
                select node;

            if (!commentNodes.Any())
            {
                return;
            }

            foreach (var node in commentNodes)
            {
                string commentText = "";
                switch (node.Kind())
                {
                    case SyntaxKind.SingleLineCommentTrivia:
                        commentText = node.ToString().TrimStart('/');
                        break;
                    case SyntaxKind.MultiLineCommentTrivia:
                        var nodeText = node.ToString();

                        commentText = nodeText.Substring(2, nodeText.Length - 4);
                        break;
                }

                if (commentText.Length > 255 || commentText.Contains(";") ||
                    commentText.Contains("{") || commentText.Contains("}"))
                {
                    var diagnostic = Diagnostic.Create(Rule, node.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
