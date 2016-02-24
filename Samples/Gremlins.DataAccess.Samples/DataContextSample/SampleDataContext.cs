namespace Gremlins.DataAccess.Samples.DataContextSample
{
    class SampleDataContext: DataContext
    {
        public SampleDataContext()
            :base(new SampleConnectionProvider())
        { }
    }
}
