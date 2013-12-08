The .NET Framework doesn't correctly determine the [ISO 8601](http://www.iso.org/iso/home/standards/iso8601.htm) compliant number of a week within a year, as outlined in http://support.microsoft.com/kb/200299. This is a problem when working within the EU especially, as most countries here use ISO 8601 week numbers.

Although there are a lot of code samples to be found on e.g. Stack Overflow solving the problem, I found most of them to involve too many [magic numbers](http://en.wikipedia.org/wiki/Magic_number_%28programming%29#Unnamed_numerical_constants) to be acceptable for production code.

The following functionality is inspired by the concept of [week dates](http://en.wikipedia.org/wiki/ISO_week_date) and is comprised of a `Week` and a `WeekDate` class.