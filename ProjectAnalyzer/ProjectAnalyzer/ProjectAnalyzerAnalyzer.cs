using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace ProjectAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ProjectAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public
        const string DiagnosticId = "CustomAnalyzer";
        private static readonly LocalizableString Title = new
        LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager,
          typeof(Resources));
        private static readonly LocalizableString MessageFormat = "Comment was found";
        private static readonly LocalizableString Description = "Comment was found";
        private const string Category = "Usage";
        private static readonly DiagnosticDescriptor Rule = new
        DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category,
          DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxTreeAction(HandleSyntaxTree);
        }
        private void HandleSyntaxTree(SyntaxTreeAnalysisContext context)
        {
            SyntaxNode root =
              context.Tree.GetCompilationUnitRoot(context.CancellationToken);
            System.Collections.Generic.IEnumerable<SyntaxTrivia> commentNodes = from node in root.DescendantTrivia()
                                                                                where
                                                                                node.IsKind(SyntaxKind.MultiLineCommentTrivia) ||
                                                                                  node.IsKind(SyntaxKind.SingleLineCommentTrivia)
                                                                                select node;
            if (!commentNodes.Any())
            {
                return;
            }
            foreach (SyntaxTrivia node in commentNodes)
            {
                string commentText = "";
                switch (node.Kind())
                {
                    case SyntaxKind.SingleLineCommentTrivia:
                        commentText = node.ToString().TrimStart('/');
                        break;
                    case SyntaxKind.MultiLineCommentTrivia:
                        string nodeText = node.ToString();
                        commentText = nodeText.Substring(2, nodeText.Length - 4);
                        break;
                }
                if (commentText.Length > 255 || commentText.Contains(";") ||
                  commentText.Contains("{") || commentText.Contains("}"))
                {
                    Diagnostic diagnostic = Diagnostic.Create(Rule, node.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}