namespace ConsoleSteps.ModelState
{
    public abstract class AbstractState
    {
        protected Context _context;

        public void SetContext(Context context)
        {
            this._context = context;
        }

        public abstract void Resolve();

    }
}
