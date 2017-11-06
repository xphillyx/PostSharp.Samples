using System;
using PostSharp.Patterns.Caching.Dependencies;

namespace PostSharp.Samples.Caching
{
  [Serializable]
  internal class Account : ICacheDependency
  {
    public int AccountId;

    public string GetCacheKey()
    {
      return $"Account:{AccountId}";
    }

    public bool Equals(ICacheDependency other)
    {
      throw new NotImplementedException();
    }
  }
}