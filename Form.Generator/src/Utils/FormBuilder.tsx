import React from "react";
import {FormItem} from "../Core/IForm.ts";

export class FormBuilder {
    private formElements: React.ReactNode[] = [];

    addTitle(title: string): FormBuilder {
        this.formElements.push(
            <div className="flex flex-col space-y-2 pb-4 w-full">
            <h2 className="text-3xl font-semibold text-gray-800">
                {title}
            </h2>
        </div>)

        return this
    }

    addFiller(fillerContent: string ): FormBuilder {

        this.formElements.push(
            <div className="w-full" dangerouslySetInnerHTML={{ __html: fillerContent }}>
            </div>
        )

        return this;
    }

    addTextInput(item: FormItem): FormBuilder {
        this.formElements.push(
            <div className="flex flex-col space-y-2 pb-4">
                <label htmlFor="name" className="text-sm font-semibold">{item.label}</label>
                <input
                    type={item.validationRules?.type}
                    name="name"
                    id="name"
                    placeholder={item.placeholder ? item.placeholder : ""}
                    required={item.required}
                    className={`${item.class} border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-orange-300 font-medium`}
                    pattern={item.validationRules?.type === 'tel' ? "[0-9]" : "*"}
                />
            </div>
        )
        return this
    }

    addRadio(item: FormItem): FormBuilder {
        this.formElements.push(
            <div className="flex flex-col space-y-2 pb-4">
                <label className="text-sm font-semibold">{item.label}</label>
                <div className="space-y-1">
                    {item.items!.map((radioItem) => (
                        <label key={radioItem.value} className="flex items-center">
                            <input
                                type="radio"
                                name="radio"
                                value={radioItem.value}
                                className="form-radio"
                                defaultChecked={radioItem.checked}
                                required={item.required}
                            />
                            <span className="ml-2">{radioItem.label}</span>
                        </label>
                    ))}
                </div>
            </div>
        )

        return this;
    }

    addSelect(item: FormItem): FormBuilder {
        this.formElements.push(
            <div className="flex flex-col space-y-2 pb-4">
                <label className="text-sm font-semibold">{item.label}</label>
                <select required={item.required} className="border border-gray-300 rounded-md p-2 font-medium" defaultValue={item.options?.find(o => o.selected)?.value}>
                    {item.options!.map((option) => (
                        <option
                            key={option.value}
                            value={option.value}
                        >
                            {option.text}
                        </option>
                    ))}
                </select>
            </div>
        )

        return this;
    }

    addButton(text: string, classStyle: string): FormBuilder {
        this.formElements.push(
            <button
                type="submit"
                className={`${classStyle} w-full flex items-center justify-center bg-orange-500 text-white px-4 py-2 rounded-xl cursor-pointer font-medium hover:bg-orange-600 gap-2`}
            >
                {text}
            </button>
        )
        return this;
    }

    addCheckbox(item: FormItem): FormBuilder {
        this.formElements.push(
            <div className="w-full pb-4">
                <label className="flex items-center space-x-2 w-full text-sm font-semibold break-words">
                    <input
                        type="checkbox"
                        name={item.name}
                        defaultChecked={item.checked}
                        required={item.required}
                        className={`form-checkbox ${item.class}`}
                        disabled={item.disabled}
                    />
                    <span className="flex-1">{item.label}</span>
                </label>
            </div>
        );

        return this;
    }


    addTextAreaInput(item: FormItem): FormBuilder {
        this.formElements.push(
            <div className="flex flex-col space-y-2 pb-4">
                <label htmlFor={item.name} className="text-sm font-semibold">{item.label}:</label>
                <textarea
                    name={item.name}
                    id={item.name}
                    placeholder={item.placeholder ? item.placeholder : ""}
                    required={item.required}
                    className={`${item.class} border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-orange-300 font-medium`}
                    rows={4}
                />
            </div>
        );
        return this;
    }

    buildForm(): React.ReactNode[] {
        return this.formElements;
    }
}