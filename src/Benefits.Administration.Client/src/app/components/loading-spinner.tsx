import React from 'react';
import { Spinner } from 'react-bootstrap';
import classnames from 'classnames';

import './loading-spinner.scss'

export type LoadingProps = 'idle' | 'pending' | 'succeeded' | 'failed';

type Props = {
    loading: LoadingProps;
    error?: React.ReactNode;
    className?: string;
}

export default class LoadingSpinner extends React.Component<Props>
{    
    render() {
        const { loading, children, error } = this.props;

        if (loading == 'failed')
            return error || <div>There was an error while loading.</div>

        if (loading == 'pending') 
            return this.Spinner;

        if (loading == 'succeeded')
            return children;

        // idle
        return null;
    }

    private get Spinner(): React.ReactNode
    {
        return (
            <div className={classnames('loading-spinner', this.props.className)}>
                <Spinner animation="border" role="status" />
                <span>Loading...</span>
            </div>
        )
    }
}