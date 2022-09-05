     namespace CodeAnalysis {
    class Evaluator {
          public ExpressionSyntax Root;
          public Evaluator(ExpressionSyntax root){
             Root = root;
          }
          public int Evaluate(){
            return EvaluateExpression(Root );
          }
          public int EvaluateExpression(ExpressionSyntax node){
            if (node is NumberExpressionSyntax n ){
                  return (int) n.NumberToken._value;
            } 
            if(node is BinaryExpressionSyntax b){
            
              var left = EvaluateExpression(b.Left);
              var right = EvaluateExpression(b.Right);
              if(b.OperatorToken.Kind == SyntaxKind.PlusToken)
                  return left + right;
              else if(b.OperatorToken.Kind == SyntaxKind.MinusToken)
                  return left - right;
              else if(b.OperatorToken.Kind == SyntaxKind.StarToken)
                  return left * right;
              else if(b.OperatorToken.Kind == SyntaxKind.SlashToken)
                  return left / right;
              else 
                  throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
            }
            if(node is ParenthesisizedExpessionSyntax p){
                 return EvaluateExpression(p.expression);
            }
            throw new Exception($"Unexpected node {node.Kind}");
          }
   }
     }