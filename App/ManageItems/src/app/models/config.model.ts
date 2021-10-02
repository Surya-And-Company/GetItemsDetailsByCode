export class Config {
    public static specialKeys: Array<string> = ['Backspace', 'Tab', 'End', 'Home', '-'];
    public static  numberRegex: RegExp = new RegExp(/^-?(0|[1-9]\d*)?$/g);
    public static decimalRegex: RegExp = new RegExp(/^-?[0-9]+(\.[0-9]*){0,1}$/g);
    public static passwordRegex : RegExp = new RegExp(/((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/);
    public static mobileRegex: RegExp = new RegExp(/^\d{10}$/);
    public static otpRegex: RegExp = new RegExp(/^\d{6}$/);
}
