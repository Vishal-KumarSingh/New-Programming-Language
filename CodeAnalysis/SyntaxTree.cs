  namespace CodeAnalysis {
    sealed class SyntaxTree {
      public ExpressionSyntax Root;
      SyntaxToken EndofFileToken;
      public IEnumerable<string> Diagnostics;
      public SyntaxTree(IEnumerable<string> diagnostics ,  ExpressionSyntax root , SyntaxToken endoffiletoken){
        Diagnostics = diagnostics;
        Root = root;
        EndofFileToken = endoffiletoken;
      }
    }
  }