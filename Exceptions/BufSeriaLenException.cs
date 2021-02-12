using System;


namespace BufSeria
{
  public class BufSeriaLenException : Exception
  {
    public BufSeriaLenException()
    {
    }

    public BufSeriaLenException(string message)
        : base(message)
    {
    }

    public BufSeriaLenException(string message, Exception inner)
        : base(message, inner)
    {
    }
  }
}
