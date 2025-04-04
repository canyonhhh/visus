import RandomNumber from "../components/RandomNumber"
const Home = () => {
    return (
        <section className="bg-[#EEEEEE] py-16 text-center min-h-screen">
            <h1 className="text-4xl font-bold pb-8">Home page</h1>
            <RandomNumber/>
        </section>
    );
};

export default Home;
