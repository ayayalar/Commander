namespace Commander.Test.Data
{
    internal class Repo
    {
        public static Repo Instance() => new Repo();
        public Model GetModel(string name) => new Model(1, name);
    }
}
