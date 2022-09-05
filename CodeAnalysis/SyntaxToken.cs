   namespace CodeAnalysis {
    class SyntaxToken : SyntaxNode  {
          public object _value {get;}
          public override SyntaxKind Kind {get;}
          public int Position;
          public string Text;
          public SyntaxToken(SyntaxKind kind , int position , string text , object value){
                  Kind = kind;
                  Position = position;
                  Text = text;
                  _value = value;
          }
         public override IEnumerable<SyntaxNode> GetChildren(){
            return Enumerable.Empty<SyntaxNode>();
         }
    } }