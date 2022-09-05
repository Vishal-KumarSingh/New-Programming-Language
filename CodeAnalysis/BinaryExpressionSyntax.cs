  namespace CodeAnalysis {
    
    sealed class BinaryExpressionSyntax : ExpressionSyntax {
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public ExpressionSyntax Left, Right;
        public SyntaxToken OperatorToken;

        public BinaryExpressionSyntax(ExpressionSyntax left , SyntaxToken operatorToken , ExpressionSyntax right){
              Left = left ;
              Right = right;
              OperatorToken = operatorToken;
        }
           public override IEnumerable<SyntaxNode> GetChildren(){
            yield return Left;
            yield return OperatorToken;
            yield return Right;
         }
    }
  }