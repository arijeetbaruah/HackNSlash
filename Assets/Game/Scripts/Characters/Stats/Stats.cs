namespace Baruah.HackNSlash.Characters.Stats
{
    [System.Serializable]
    public struct Stats
    {
        public int Attack;
        public int Defence;
        public int MagicAttack;
        public int MagicDefence;
        public int Constitution;
        public int Luck;

        public static Stats operator+(Stats a, Stats b)
        {
            Stats c = new Stats();

            c.Attack = a.Attack + b.Attack;
            c.Defence = a.Defence + b.Defence;
            c.MagicAttack = a.MagicAttack + b.MagicAttack;
            c.MagicDefence = a.MagicDefence + b.MagicDefence;
            c.Constitution = a.Constitution + b.Constitution;
            c.Luck = a.Luck + b.Luck;
            
            return c;
        }
    }
}
