public class ValuesEvent
{
    public int Value {get;}
    public string UserInput {get;}
    public ValuesEvent(int value, string userInput)
    {
        UserInput = userInput;
        Value = value;
    }
}