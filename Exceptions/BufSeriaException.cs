using System;


namespace BufSeria
{
  public class BufSeriaException : Exception
  {
    public BufSeriaException()
    {
    }

    public BufSeriaException(string message)
        : base(message)
    {
    }

    public BufSeriaException(string message, Exception inner)
        : base(message, inner)
    {
    }
  }
}
