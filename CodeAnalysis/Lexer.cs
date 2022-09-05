   namespace CodeAnalysis {
    class Lexer {
        private readonly string Text;
        private List<string> _diagnostics  = new List<string>();
        public int Position;
        public char Current {
            get {
                if(Position >= Text.Length)
                   return '\n';
                return Text[Position];
            }
        }
        public Lexer(string text){
              Text = text;
        }
        public IEnumerable<string> Diagnostics => _diagnostics;
        private void Next(){
            Position++;
        }
        public SyntaxToken nextToken(){
              // <numbers>
              // + - * / ()
              // whitespaces
              if (Position >= Text.Length){
                return new SyntaxToken(SyntaxKind.EndOfFileToken , Position , "\0" , null);
              }
              if(char.IsDigit(Current)){
                  var start = Position;
                  while(char.IsDigit(Current))
                          Next();
                  
                 var length = Position - start;
                 var text = Text.Substring(start , length);
                 if(!int.TryParse(text , out var value)){
                      _diagnostics.Add($"The number {text} cannot be represented as Int32");
                 }
                 return new SyntaxToken(SyntaxKind.NumberToken , start , text, value );
              }
              if(char.IsWhiteSpace(Current))
              {
                var start = Position;
                  while(char.IsWhiteSpace(Current))
                          Next();
                 var length = Position - start;
                 var text = Text.Substring(start , length);
                 int.TryParse(text , out var value);
                 return new SyntaxToken(SyntaxKind.WhitespaceToken , start , text , value);
              }
              if(Current == '+'){
                return new SyntaxToken(SyntaxKind.PlusToken , Position++ , "+" , null);
              }
              if(Current == '-'){
                return new SyntaxToken(SyntaxKind.MinusToken , Position++ , "-" , null);
              }
              if(Current == '*'){
                return new SyntaxToken(SyntaxKind.StarToken , Position++ , "*" , null);
              }
              if(Current == '/'){
                return new SyntaxToken(SyntaxKind.SlashToken , Position++ , "/" , null);
              }
              if(Current == '('){
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken , Position++ , "(" , null);
              }
              if(Current == ')'){
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken , Position++ , ")" , null);
              }
              _diagnostics.Add($"ERROR: bad character input: {Current}");
              return new SyntaxToken(SyntaxKind.BadToken , Position++ , Text.Substring(Position-1 , 1) , null);
        }

    }
   }
   