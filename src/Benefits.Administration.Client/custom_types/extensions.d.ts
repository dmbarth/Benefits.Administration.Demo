interface Number {
    
    /**
     * @param {string} [outformat] Optional out format that defaults to '$0,0.[00]'
     * 
     * @returns {string} The formatted currency string
     */
    toCurrency: (this: number, outformat?: string) => string;

    /**
     * @param {string} [outformat] Optional out format that defaults to '0,0.[00]'
     * 
     * @returns {string} The formatted number string
     */
    format: (this: number, outformat?: string) => string;
}