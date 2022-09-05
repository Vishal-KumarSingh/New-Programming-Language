using System;
using CodeAnalysis;

namespace compiler {
    class Translator {
         static void Main(){
                Console.WriteLine(">");  
                var line = Console.ReadLine();
                if ( string.IsNullOrWhiteSpace(line)){
                    return;
                }
                 var syntaxTree = Parser.Parse(line);
                 var color = Console.ForegroundColor;
                 Console.ForegroundColor = ConsoleColor.DarkGray;
                PrettyPrint(syntaxTree.Root);
                var lexer = new Lexer(line);
                if(syntaxTree.Diagnostics.Any()){
                  Console.ForegroundColor = ConsoleColor.DarkRed;
                  foreach( var diagtnostics in syntaxTree.Diagnostics){
                     Console.Write(diagtnostics);
                  }
                  Console.ForegroundColor = color;
                }else{
                  var e = new Evaluator(syntaxTree.Root);
                  var result = e.Evaluate();
                  Console.WriteLine(result);
                }
         }

         static void PrettyPrint(SyntaxNode node , string indent = "" , bool isLast = false){
            Console.Write(indent);
            Console.Write(node.Kind);
            if(node is SyntaxToken t && t._value != null){
                Console.Write(" ");
                Console.Write(t._value);

            }
            Console.WriteLine("");

             //indent += "     ";
            
            indent += isLast ? "|-" : "|--";
             
             var lastChild = node.GetChildren().LastOrDefault();
             foreach(var child in node.GetChildren()){
                PrettyPrint(child , indent , node== lastChild);

             }
         }
    }
   
 }

