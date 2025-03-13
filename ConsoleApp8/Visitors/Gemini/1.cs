namespace ConsoleApp1Visitor.Gemini.WithoutPattern;
/*
 * فرض کنید می‌خواهیم یک سیستم ساده برای فروشگاه کتاب طراحی کنیم.
 * در این سیستم، انواع مختلفی از کتاب (مانند رمان، کتاب فنی، کتاب کودک) وجود دارد
 * و هر نوع کتاب ویژگی‌های خاص خود را دارد (مانند تعداد صفحه، ژانر، گروه سنی).
 * همچنین می‌خواهیم بتوانیم عملیات مختلفی را روی این کتاب‌ها انجام دهیم، مثل محاسبه قیمت کل، چاپ اطلاعات کتاب، و اعمال تخفیف‌های مختلف.
 */

//پیاده‌سازی بدون استفاده از الگوی بازدیدکننده
public abstract class Book
{
    public string Title { get; set; }
    public double Price { get; set; }
    // سایر خصوصیات
    public abstract double CalculatePrice();
    public abstract void PrintInfo();
}

public class Novel : Book
{
    // خصوصیات خاص رمان
    public override double CalculatePrice()
    {
        // محاسبه قیمت رمان
        return 5;
    }
    public override void PrintInfo()
    {
        // چاپ اطلاعات رمان
    }
}

public class TechnicalBook : Book
{
    // خصوصیات خاص کتاب فنی
    public override double CalculatePrice()
    {
        // محاسبه قیمت کتاب فنی
        return 2.0;
    }
    public override void PrintInfo()
    {
        // چاپ اطلاعات کتاب فنی
    }
}

// ... سایر انواع کتاب

// کلاس مشتری
public class Customer
{
    public List<Book> Books { get; set; }
    public double CalculateTotalPrice()
    {
        double totalPrice = 0;
        foreach (var book in Books)
        {
            totalPrice += book.CalculatePrice();
        }
        return totalPrice;
    }
}

/*
 * در این پیاده‌سازی، هر نوع کتاب متدهای CalculatePrice و PrintInfo خود را دارد. اگر بخواهیم عملیات جدیدی مانند اعمال تخفیف اضافه کنیم، باید این متدها را در همه کلاس‌های کتاب اضافه و تغییر دهیم که باعث تکرار کد و کاهش انعطاف‌پذیری می‌شود.


*/