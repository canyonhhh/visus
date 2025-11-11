class BusinessesController < ApplicationController
  before_action :set_business, only: %i[ show edit update destroy ]

  def index
    @businesses = Business.all
  end

  def show
  end

  def new
    @business = Business.new
  end

  def edit
  end

  def create
    @business = Business.new(business_params)

    if @business.save
      redirect_to @business, notice: "Business was successfully created."
    else
      render :new, status: :unprocessable_entity
    end
  end

  def destroy
    @business.destroy!

    redirect_to businesses_path, notice: "Business was successfully destroyed.", status: :see_other
  end

  private
  def set_business
    @business = Business.find(params.expect(:id))
  end

  def business_params
    params.expect(business: [ :name, :contact_info ])
  end
end
