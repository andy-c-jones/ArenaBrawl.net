using System;

namespace ArenaBrawl
{
    public class PlayerSession
    {
        public PlayerSession()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}