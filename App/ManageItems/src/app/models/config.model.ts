export class Config {
    public static specialKeys: Array<string> = ['Backspace', 'Tab', 'End', 'Home', '-'];
    public static  numberRegex: RegExp = new RegExp(/^-?(0|[1-9]\d*)?$/g);
    public static decimalRegex: RegExp = new RegExp(/^\d*\.?\d{0,2}$/g);
    public static noSpaceBeginingEnd: RegExp = new RegExp(/^[^\s]/);
    public static passwordRegex : RegExp = new RegExp(/((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/);
    public static mobileRegex: RegExp = new RegExp(/^\d{10}$/);
    public static otpRegex: RegExp = new RegExp(/^\d{6}$/);
}
