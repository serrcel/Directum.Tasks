export interface IForm {
    name: string;
    items: FormItem[];
    postmessage: string;
}

export interface IFormData {
    form: IForm;
}

export interface FormItem {
    type: "filler" | "text" | "textarea" | "checkbox" | "button" | "radio" | "select";
    name?: string;
    message?: string;
    placeholder?: string;
    required?: boolean;
    validationRules?: IValidationRules;
    value?: string;
    label?: string;
    class?: string;
    disabled?: boolean;
    checked?: boolean;
    text?: string;
    items?: IRadioItems[]
    options?: ISelectItems[]
}

interface IValidationRules {
    type: string;
}

interface IRadioItems {
    value: string;
    label: string;
    checked: boolean;
}

interface ISelectItems {
    value: string;
    text: string;
    selected: boolean;
}