import React from "react";
import { Card, Col, Row } from "react-bootstrap";

import _ from 'lodash';

type Props = {
    title: string;
    cost: number;
    discounts: number[];
    total: number;
    costMultiplier?: number;
}

export default class DeductionCard extends React.Component<Props> {
    render() {
        const { title, cost, discounts, total, costMultiplier } = this.props;

        return (
            <Card className='mt-3'>
                <Card.Body>
                    <Card.Title>{title}</Card.Title>
                    <Row>
                        <Col sm="6">Cost {this.multiplier(costMultiplier)}</Col>
                        <Col className='text-right'>{cost.format()}</Col>
                    </Row>
                    <Row>
                        <Col sm="6">Discounts {this.multiplier(discounts)}</Col>
                        <Col className='text-right'>{_.sum(discounts).format()}</Col>
                    </Row>
                    <Row>
                        <Col sm="6">Total</Col>
                        <Col className='text-right'>{total.toCurrency()}</Col>
                    </Row>
                </Card.Body>
            </Card>
        )
    }
    
    multiplier(amounts: any[] | number): string {
        if (!amounts)
            return null;

        if (typeof amounts == 'number')
            return `${amounts}x`;

        return amounts.length > 0 ?
            `${amounts.length}x` : null;
    }
}