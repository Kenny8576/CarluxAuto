import React from 'react'
import { UseControllerProps, useController } from 'react-hook-form'
import DatePicker, { DatePickerProps } from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

type Props = {
    label: string
    type?: string
    showLabel?: boolean
} & UseControllerProps & Partial<DatePickerProps>

export default function DateInput(props: Props) {
    const {fieldState, field} = useController({...props, defaultValue: ''})

    return (
        <div className='block'>
            <DatePicker
                selected={field.value as Date | null} // Ensure correct type
                onChange={(date: Date | null) => field.onChange(date)}
                placeholderText={props.label}
                className={`rounded-lg w-[100%] flex flex-col
                    ${fieldState.error
                    ? "bg-red-50 border-red-500 text-red-900"
                    : fieldState.isDirty
                    ? "bg-green-50 border-green-500 text-green-900"
                    : ""}`}
                dateFormat=" dd MMMM yyyy h:mm a" // Specify the format
                showTimeSelect
                dropdownMode="select"
                {...(props.disabled !== undefined ? { disabled: props.disabled } : {})} // Only pass disabled if defined
                />

                {fieldState.error && (
                    <div className='text-red-500 text-sm'>{fieldState.error.message}</div>
                )}
        </div>
    )
}
