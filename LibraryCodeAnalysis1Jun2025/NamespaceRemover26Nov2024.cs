using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

// see solution SolutionCsharpGloVe3Sep2024 project LibraryCodeAnalysis30Nov2024
namespace LibraryCodeAnalysis1Jun2025
{
    public class NamespaceRemover26Nov2024 : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
        {
            // Flatten namespace members into the global scope
            var newMembers = node.Members.SelectMany<MemberDeclarationSyntax, MemberDeclarationSyntax>(member =>
            {
                if (member is NamespaceDeclarationSyntax namespaceDecl)
                {
                    // Extract the members of the namespace
                    return namespaceDecl.Members;
                }

                // Keep the member as-is if it's already in the global scope
                return new[] { member };
            });

            // Create a new CompilationUnitSyntax with all members in the global scope
            return node.WithMembers(SyntaxFactory.List(newMembers));
        }
    }
}
