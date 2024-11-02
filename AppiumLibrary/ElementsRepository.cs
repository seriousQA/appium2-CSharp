namespace AppiumLibrary;

public class ElementsRepository
{
    private static string result;

    //// <summary> Calculator: digit 0 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit0(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_0";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_0\"]";
        }
        return result;
    }

    //// <summary> Calculator: digit 1 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit1(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_1";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_1\"]";
        }
        return result;
    }

    //// <summary> Calculator: digit 2 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit2(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_2";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_2\"]";
        }
        return result;
    }

    //// <summary> Calculator: digit 3 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit3(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_3";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_3\"]";
        }
        return result;
    }

    //// <summary> Calculator: digit 4 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit4(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_4";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_4\"]";
        }
        return result;
    }

    //// <summary> Calculator: digit 5 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit5(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_5";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_5\"]";
        }
        return result;
    }

    //// <summary> Calculator: digit 6 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit6(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_6";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_6\"]";
        }
        return result;
    }

    /// <summary> Calculator: digit 7 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit7(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_7";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_7\"]";
        }
        return result;
    }

    /// <summary> Calculator: digit 8 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit8(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_8";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_8\"]";
        }
        return result;
    }

    /// <summary> Calculator: digit 9 Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_digit9(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/digit_9";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@resource-id=\"com.android.calculator2:id/digit_9\"]";
        }
        return result;
    }

    /// <summary>  Calculator: add ImageView. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_add(string locator)
    {
        if (locator.Equals("accessibility id"))
        {            
            result = "plus";
        }
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/op_add";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.ImageView[@content-desc=\"plus\"]";
        }
        return result;
    }

    /// <summary>  Calculator: sub ImageView. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_sub(string locator)
    {
        if (locator.Equals("accessibility id"))
        {            
            result = "minus";
        }
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/op_sub";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.ImageView[@content-desc=\"minus\"]";
        }
        return result;
    }

    /// <summary>  Calculator: div ImageView. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_div(string locator)
    {
        if (locator.Equals("accessibility id"))
        {            
            result = "dividieren";
        }
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/op_div";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.ImageView[@content-desc=\"dividieren\"]";
        }
        return result;
    }

    /// <summary>  Calculator: mul ImageView. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_mul(string locator)
    {
        if (locator.Equals("accessibility id"))
        {            
            result = "Mal";
        }
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/op_mul";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.ImageView[@content-desc=\"Mal\"]";
        }
        return result;
    }

    /// <summary> Calculator: equal ImageView. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_eq (string locator)
    {
        if (locator.Equals("accessibility id"))
        {            
            result = "ist gleich";
        }
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/eq";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.ImageView[@content-desc=\"ist gleich\"]";
        }
        return result;
    }

    /// <summary> Calculator: formula EditText. </summary>
    /// <param name="locator"> string locator. </param>
    public static string edt_formula (string locator)
    {
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/formula";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.EditText[@resource-id=\"com.android.calculator2:id/formula\"]";
        }
        return result;
    }

    /// <summary>  Calculator: C (clear) ImageView. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_clear(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/op_clr";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.ImageView[@resource-id=\"com.android.calculator2:id/op_clr\"]";
        }
        return result;
    }

    /// <summary>  Calculator: delete ImageView. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_delete(string locator)
    {        
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/del";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.ImageView[@resource-id=\"com.android.calculator2:id/del\"]";
        }
        return result;
    }

    /// <summary> Calculator: percentage Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_pct (string locator)
    {
        if (locator.Equals("accessibility id"))
        {            
            result = "Prozent";
        }
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/op_pct";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@content-desc=\"Prozent\"]";
        }
        return result;
    }

    /// <summary> Calculator: decimal point Button. </summary>
    /// <param name="locator"> string locator. </param>
    public static string btn_dec_point (string locator)
    {
        if (locator.Equals("accessibility id"))
        {            
            result = "Dezimalzeichen";
        }
        if (locator.Equals("id"))
        {            
            result = "com.android.calculator2:id/dec_point";
        }
        if (locator.Equals("xpath"))
        {
            result = "//android.widget.Button[@content-desc=\"Dezimalzeichen\"]";
        }
        return result;
    }

}