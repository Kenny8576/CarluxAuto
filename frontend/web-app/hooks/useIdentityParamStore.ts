import { createWithEqualityFn } from 'zustand/traditional';

type State = {
    firstname: string;
    lastname: string;
    email: string;
    password: string;
    social?: string;
};

type Actions = {
    setParams: (param: Partial<State>) => void;
    reset: () => void;
};

const initialState: State = {
    firstname: '',
    lastname: '',
    email: '',
    password: '',
    social: ''
};

export const useIdentityParamStore = createWithEqualityFn<State & Actions>()((set) => ({
    ...initialState,

    setParams: (newParams) => {
        set((state) => ({
            ...state,
            ...newParams,
        }));
    },

    reset: () => {
        set(() => ({
            ...initialState
        }));
    },
}));
