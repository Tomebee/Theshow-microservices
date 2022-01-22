import * as React from 'react';
import { 
    FASTDesignSystemProvider, 
    FASTCard, 
    FASTButton,
    FASTMenu
  } from '@microsoft/fast-components';

type DefaultFast = React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement> & any;

declare global {
    namespace JSX {
        interface IntrinsicElements {
            /**
             *  React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement> allows setting standard HTML attributes on the element
             */
            "fast-design-system-provider": DefaultFast;
            "fast-card": DefaultFast;
            "fast-button": any;
            "fast-select": React.DetailedHTMLProps<React.SelectHTMLAttributes<HTMLSelectElement>, HTMLSelectElement>;
            "fast-option": React.DetailedHTMLProps<React.OptionHTMLAttributes<HTMLOptionElement>, HTMLOptionElement>;
            "fast-text-field": React.DetailedHTMLProps<React.InputHTMLAttributes<HTMLInputElement>, HTMLInputElement>;
            "fast-progress-ring": DefaultFast;
        }
    }
}

