  namespace CodeAnalysis {
    class Parser {
        private SyntaxToken Current => Peek(0);
        private List<string> _diagnostics = new List<string>();
        public IEnumerable<string> Diagnostics => _diagnostics;
        private readonly SyntaxToken[] _tokens;
        public int Position;
        public static SyntaxTree Parse(string text){
              var parser = new Parser(text);
              return parser.Parse();
        }
         public Parser(string text){
                var lexer = new Lexer(text);
                var tokens = new List<SyntaxToken>();
                SyntaxToken token;
                do {
                    token = lexer.nextToken();
                    if(token.Kind != SyntaxKind.WhitespaceToken || token.Kind != SyntaxKind.BadToken){
                        tokens.Add(token);
                    }
                }while (token.Kind != SyntaxKind.EndOfFileToken);
                _tokens = tokens.ToArray();
                _diagnostics.AddRange(lexer.Diagnostics);
         }
         private SyntaxToken NextToken()
         {
            var current = Current;
            Position++;
            return current;
         }
      private SyntaxToken Peek(int offset){
        var index = Position + offset;
        if( index >= _tokens.Length){
            return _tokens[_tokens.Length - 1];
        }
        return _tokens[index];
      }
      private SyntaxToken Match(SyntaxKind kind)
    {
       if(Current.Kind == kind)
           return NextToken();
        _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}> expected <{kind}>");
        return new SyntaxToken(kind , Current.Position , null , null);
    }
    public ExpressionSyntax ParseTerm(){
           var left = ParseFactor();
         
           while(Current.Kind == SyntaxKind.PlusToken ||
                 Current.Kind == SyntaxKind.MinusToken
                ){
            var operatorToken = NextToken();
            var right = ParseFactor();
            left = new BinaryExpressionSyntax(left , operatorToken , right);
          }

          
          return left;
    }
      public ExpressionSyntax ParseFactor(){
            var left = ParsePrimaryExpression();
         
           while(Current.Kind == SyntaxKind.StarToken || 
                 Current.Kind == SyntaxKind.SlashToken){
            var operatorToken = NextToken();
            var right = ParsePrimaryExpression();
            left = new BinaryExpressionSyntax(left , operatorToken , right);
          }

          
          return left;
      }
      private ExpressionSyntax ParseExpression(){
        return ParseTerm();
      }
      public SyntaxTree Parse(){
         var expression = ParseTerm();
         var endoffiletoken =  Match(SyntaxKind.EndOfFileToken);
         return new SyntaxTree(_diagnostics , expression , endoffiletoken);
      }
      private ExpressionSyntax ParsePrimaryExpression(){
          if(Current.Kind == SyntaxKind.OpenParenthesisToken){
            var left = NextToken();
            var expression = ParseExpression();
            var right = Match(SyntaxKind.CloseParenthesisToken);
            return new ParenthesisizedExpessionSyntax(left , expression , right);
          }
          var numberToken = Match(SyntaxKind.NumberToken);
          return new NumberExpressionSyntax(numberToken);
      }
    }
  }