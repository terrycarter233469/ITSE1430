
//Option 1
//inefficient as it creates a new memory location for each concat
//Compiler automatically converts this code into option 3 automatically up to 6 strings
string names = "John" + " William" + "Murphy" + " Charles" + " Henry"

//Option 2
//Most efficient method for large amounts of strings
StringBuilder builder = new StringBuilder();
builder.Append("John");
builder.Appent(" William);
string names2 = builder.ToString();

//Option 2
//under the hood uses the stringbuilder
//most efficient, readable method for up to 6 strings
string names3 = String.Concat("John", " William", "Murphy", " Charles", " Henry");

//String formatting
//John worked 10 hours
string format1 = name + " worked " + hours.ToString() + " hours";

string fromat2 = String.Format("{0} worked for {1} hours", name, hours);	//format string
