namespace AjErl.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IForm
    {
        object Evaluate(Context context);
    }
}
