     namespace CodeAnalysis {
    sealed class ParenthesisizedExpessionSyntax : ExpressionSyntax {
        public SyntaxToken OpenParenthesisToken { get;}
        public SyntaxToken CloseParentehsisToken{get;}
        public ExpressionSyntax expression{get;}
        public override SyntaxKind Kind => SyntaxKind.ParenthesiszedExpession;
         public ParenthesisizedExpessionSyntax(SyntaxToken openParenthesisToken , ExpressionSyntax expression , SyntaxToken closeParenthesisToken){
              OpenParenthesisToken = openParenthesisToken;
              CloseParentehsisToken= closeParenthesisToken;
              this.expression = expression;

         }
         public override IEnumerable<SyntaxNode> GetChildren(){
          yield return OpenParenthesisToken;
          yield return expression;
          yield return CloseParentehsisToken;
         }
    }
     }