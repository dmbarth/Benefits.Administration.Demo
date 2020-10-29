import numeral from 'numeral';

Number.prototype.toCurrency = function(this: number, outFormat?: string): string {
    return outFormat ? 
        numeral(this).format(outFormat) : 
        numeral(this).format('$0,0.[00]');
}

Number.prototype.format = function(this: number, outFormat?: string): string {
    return outFormat ?
        numeral(this).format(outFormat) :
        numeral(this).format('0,0.[00]');
}