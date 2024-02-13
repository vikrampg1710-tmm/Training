namespace Eval;

class Tokenizer {
   public Tokenizer (Evaluator eval, string text) {
      mText = text; mN = 0; mEval = eval;
   }
   readonly Evaluator mEval;  // The evaluator that owns this 
   readonly string mText;     // The input text we're parsing through
   int mN;                    // Position within the text

   public Token Next () {
      while (mN < mText.Length) {
         char preCh = mN == 0 ? '\0' : char.ToLower (mText[mN - 1]); // Previous char
         char ch = char.ToLower (mText[mN++]);
         switch (ch, preCh) {
            case (' ' or '\t', _): continue;
            case ((>= '0' and <= '9') or '.', _): return GetNumber ();
            case ('(' or ')', _): return new TPunctuation (ch);
            case ('+' or '-' or '*' or '/' or '^' or '=', >= 'a' and <= 'z'): return new TOpArithmetic (mEval, ch);
            case ('-' or '+', not (>= '0' and <= '9')): return new TOpUnary (mEval, ch);
            case ('+' or '-' or '*' or '/' or '^' or '=', _): return new TOpArithmetic (mEval, ch);
            case ( >= 'a' and <= 'z', _): return GetIdentifier ();
            default: return new TError ($"Unknown symbol: {ch}");
         }
      }
      return new TEnd ();
   }

   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         if (ch is >= 'a' and <= 'z') continue;
         mN--; break;
      }
      string sub = mText[start..mN];
      if (mFuncs.Contains (sub)) return new TOpFunction (mEval, sub);
      else return new TVariable (mEval, sub);
   }
   readonly string[] mFuncs = { "sin", "cos", "tan", "sqrt", "log", "exp", "asin", "acos", "atan" };

   Token GetNumber () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (ch is (>= '0' and <= '9') or '.') continue;
         mN--; break;
      }
      // Now, mN points to the first character of mText that is not part of the number
      string sub = mText[start..mN];
      if (double.TryParse (sub, out double f)) return new TLiteral (f);
      return new TError ($"Invalid number: {sub}");
   }
}