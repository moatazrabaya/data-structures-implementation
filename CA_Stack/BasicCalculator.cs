namespace CA_Stack;

internal class BasicCalculator
{    
    public static long Calculate(string expression)
    {
        string postfix = InfixToPostfix(expression);

        MyStack<long> stack = new();
   
        long rightOperand = 0;
        long leftOperand = 0;
        long value = long.MinValue;
        long buffer = long.MinValue;

        void addBuffer() { stack.Push(buffer); buffer = long.MinValue; }

        foreach (char c in postfix)
        {
            if (isNumber(c))
            {
                buffer = (buffer==long.MinValue) ? 0 : buffer;
                buffer *= 10;
                buffer += c-48;
            }
            else if(c == ' ' && buffer!= long.MinValue)
            {
                addBuffer();
            }
            else if(isOperator(c))
            {
                if(buffer!= long.MinValue)
                {
                    addBuffer();
                }

                if(stack.Count>0)
                    rightOperand = stack.Pop();
                if (stack.Count > 0)
                    leftOperand = stack.Pop();

                switch(c)
                {
                    case '+': value = leftOperand + rightOperand; break;
                    case '-': value = leftOperand - rightOperand; break;
                    case '*': value = leftOperand * rightOperand; break;
                    case '/': value = leftOperand / rightOperand; break;
                    case '%': value = leftOperand % rightOperand; break;
                    case '^': value = (int)(Math.Pow(leftOperand, rightOperand)); break;
                }

                if (value != long.MinValue)
                {
                    stack.Push(value);
                    value = long.MinValue;
                }

                leftOperand = 0;
                rightOperand = 0;
            }

        }

        if (buffer != long.MinValue)
            addBuffer();

        return stack.Pop();
    }

    public static string InfixToPostfix(string expression)
    {

        if (!ValidParantheses(expression)||expression.Length==0)
            throw new InvalidOperationException("Wrong Expression");

        string returnedExpression = "";

        MyStack<char> stack = new();

        int buffer = int.MinValue;

        void resetBuffer() { returnedExpression += ' ' + buffer.ToString(); buffer = int.MinValue; }

        foreach(var item in expression)
        {
            if (isNumber(item))
            {
                buffer *= 10;
                buffer += item - 48;
            }

            else if(isOpenParantheses(item))
            {
                if(buffer!=int.MinValue)
                {
                    resetBuffer();
                }
                stack.Push(item);
            }

            else if (isCloseParantheses(item))
            {
                if (buffer != int.MinValue)
                {
                    resetBuffer();
                }

                while (stack.Count > 0 && !isOpenParantheses(stack.Peek()))
                    returnedExpression += stack.Pop();

                if(stack.Count>0)
                    stack.Pop();
            }

            else if(isOperator(item))
            {
                if (buffer != int.MinValue)
                {
                    resetBuffer();
                }

                if (item == '*' || item == '/' || item == '%')
                {
                    while (stack.Count > 0 && stack.Peek() != '+' && stack.Peek() != '-' && !isOpenParantheses(stack.Peek()))
                        returnedExpression += stack.Pop();
                }
                else if (item =='+' || item=='-')
                {
                    while (stack.Count > 0 && !isOpenParantheses(stack.Peek()))
                        returnedExpression += stack.Pop();
                }

                stack.Push(item);
            }
        }

        if(buffer != int.MinValue)
            resetBuffer();

        while (stack.Count>0)
            returnedExpression += stack.Pop();

        return returnedExpression;
    }

    public static bool ValidParantheses(string expression)
    {
        MyStack<char> stack = new();

        foreach(char c in expression)
        {
            if(isParantheses(c))
            {
                if(isCloseParantheses(c))
                {
                    if(stack.Count > 0)
                    {
                        if(
                            (c == ')' && stack.Peek() == '(') || 
                            (c == ']' && stack.Peek() == '[') || 
                            (c == '}' && stack.Peek() == '{')
                          )
                        {
                            stack.Pop();
                        }
                        else
                            return false;
                    }
                }
                else
                    stack.Push(c);
            }
        }

        return stack.Count == 0;
    }


    private static bool isOpenParantheses(char c) => c== '(' || c=='{' || c=='[';
    private static bool isCloseParantheses(char c) => c == ')' || c == '}' || c == ']';
    private static bool isParantheses(char c) => isOpenParantheses(c) || isCloseParantheses(c);
    private static bool isNumber(char c) => c>='0' && c<='9';
    private static bool isOperator(char c) => c == '+' || c == '-' || c == '*' || c=='/' || c=='%' || c=='^';
}